import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { Property } from 'src/app/core/models/property.model';
import { CloudService } from 'src/app/core/services/cloud.service';
import { PropertyService } from 'src/app/core/services/property.service';

@Component({
  selector: 'app-property-form',
  templateUrl: './property-form.component.html',
  styleUrls: ['./property-form.component.css'],
})
export class PropertyFormComponent implements OnInit {
  userId!: string;
  propertyId!: string;
  isEditMode: boolean = false;
  isLoading: boolean = false;
  propertyForm: FormGroup = new FormGroup({});
  imageUploading: boolean = false;
  uploadedImageNames: Record<string, string> = {};

  constructor(
    private formBuilder: FormBuilder,
    private propertyService: PropertyService,
    private router: Router,
    private route: ActivatedRoute,
    private cloudService: CloudService
  ) {}

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('userId')!;
    this.propertyId = this.route.snapshot.paramMap.get('propertyId')!;
    this.isEditMode = !!this.propertyId;

    this.propertyForm = this.formBuilder.group({
      title: ['', Validators.required],
      price: [0, Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      locality: ['', Validators.required],
      pincode: ['', Validators.required],
      noOfRooms: [0, Validators.required],
      carpetAreaSqft: [0, Validators.required],
      builtYear: [new Date().getFullYear(), Validators.required],
      balcony: [false],
      parking: [false],
      propertyImageUrl: [''],
      propertyImagePublicId: [''],
      hallImageUrl: [''],
      hallImagePublicId: [''],
      kitchenImageUrl: [''],
      kitchenImagePublicId: [''],
      bathroomImageUrl: [''],
      bathroomImagePublicId: [''],
      bedroomImageUrl: [''],
      bedroomImagePublicId: [''],
      parkingImageUrl: [''],
      parkingImagePublicId: [''],
    });

    if (this.isEditMode) {
      this.loadPropertyData();
    }
  }

  loadPropertyData() {
    this.propertyService.getPropertyByPropertyId(this.propertyId).subscribe({
      next: (property) => {
        this.propertyForm.patchValue(property);
      },
      error: (error) => {
        alert('Error loading property data');
      },
    });
  }

  onSubmit() {
    if (this.propertyForm.invalid) return;

    this.isLoading = true;

    const payload: Property = {
      ...this.propertyForm.value,
      userId: this.userId,
    };

    const request$: Observable<Property> = this.isEditMode
      ? this.propertyService.updateProperty(this.propertyId, payload)
      : this.propertyService.createProperty(payload);

    request$.subscribe({
      next: () => {
        this.isLoading = false;
        this.router.navigate([`/dashboard/users/${this.userId}/properties`]);
      },
      error: () => {
        this.isLoading = false;
        alert('Error creating property');
      },
    });
  }

  createProperty(property: Property) {
    this.propertyService.createProperty(property).subscribe({
      next: (property) => {
        this.isLoading = false;
        alert('Property created successfully');
        this.router.navigate([`/dashboard/users/${this.userId}/properties`]);
      },
      error: () => {
        this.isLoading = false;
        alert('Error creating property');
      },
    });
  }

  updateProperty(property: Property) {
    this.propertyService.updateProperty(this.propertyId, property).subscribe({
      next: (property) => {
        this.isLoading = false;
        alert('Property updated successfully');
        this.router.navigate([`/dashboard/users/${this.userId}/properties`]);
      },
      error: (error) => {
        this.isLoading = false;
        alert('Error updating property');
      },
    });
  }

  async onImageSelected(
    event: Event,
    type: 'property' | 'hall' | 'kitchen' | 'bathroom' | 'bedroom' | 'parking'
  ) {
    const input = event.target as HTMLInputElement;
    if (!input.files || input.files.length === 0) return;

    const originalFile = input.files[0];
    this.imageUploading = true;

    try {
      const resizedBlob = await this.resizeAndCropImage(originalFile, 140, 100);

      const resizedFile = new File(
        [resizedBlob],
        originalFile.name.replace(/\.(jpg|jpeg|png)$/i, '.jpg'),
        { type: 'image/jpeg' }
      );

      const response = await firstValueFrom(
        this.cloudService.uploadImage(resizedFile)
      );

      this.uploadedImageNames[type] = resizedFile.name;

      const patch: any = {};

      switch (type) {
        case 'property':
          patch.propertyImageUrl = response.url;
          patch.propertyImagePublicId = response.publicId;
          break;
        case 'hall':
          patch.hallImageUrl = response.url;
          patch.hallImagePublicId = response.publicId;
          break;
        case 'kitchen':
          patch.kitchenImageUrl = response.url;
          patch.kitchenImagePublicId = response.publicId;
          break;
        case 'bathroom':
          patch.bathroomImageUrl = response.url;
          patch.bathroomImagePublicId = response.publicId;
          break;
        case 'bedroom':
          patch.bedroomImageUrl = response.url;
          patch.bedroomImagePublicId = response.publicId;
          break;
        case 'parking':
          patch.parkingImageUrl = response.url;
          patch.parkingImagePublicId = response.publicId;
          break;
      }

      this.propertyForm.patchValue(patch);
    } catch (error) {
      console.error('Image upload failed', error);
      alert('Error uploading image');
    } finally {
      this.imageUploading = false;
      input.value = '';
    }
  }

  removeImage() {
    const publicId = this.propertyForm.value.propertyImagePublicId;

    if (!publicId) return;

    this.cloudService.deleteFile(publicId).subscribe({
      next: () => {
        this.propertyForm.patchValue({
          propertyImageUrl: '',
          propertyImagePublicId: '',
        });
      },
      error: (error) => {
        alert('Error deleting image');
      },
    });
  }

  private resizeAndCropImage(
    file: File,
    targetWidth: number,
    targetHeight: number
  ): Promise<Blob> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();

      reader.onload = () => {
        const img = new Image();
        img.onload = () => {
          const canvas = document.createElement('canvas');
          const ctx = canvas.getContext('2d')!;

          const targetAspect = targetWidth / targetHeight;
          const imageAspect = img.width / img.height;

          let sx = 0,
            sy = 0,
            sw = img.width,
            sh = img.height;

          if (imageAspect > targetAspect) {
            sw = img.height * targetAspect;
            sx = (img.width - sw) / 2;
          } else {
            sh = img.width / targetAspect;
            sy = (img.height - sh) / 2;
          }

          canvas.width = targetWidth;
          canvas.height = targetHeight;

          ctx.drawImage(img, sx, sy, sw, sh, 0, 0, targetWidth, targetHeight);

          canvas.toBlob(
            (blob) => {
              if (blob) resolve(blob);
              else reject('Image processing failed');
            },
            'image/jpeg',
            0.9
          );
        };

        img.onerror = reject;
        img.src = reader.result as string;
      };

      reader.onerror = reject;
      reader.readAsDataURL(file);
    });
  }
}
