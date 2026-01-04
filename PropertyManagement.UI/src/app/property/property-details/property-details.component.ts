import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Property } from 'src/app/core/models/property.model';
import { PropertyService } from 'src/app/core/services/property.service';

@Component({
  selector: 'app-property-details',
  templateUrl: './property-details.component.html',
  styleUrls: ['./property-details.component.css'],
})
export class PropertyDetailsComponent implements OnInit {
  userId!: string;
  propertyId!: string;
  isLoading: boolean = false;

  property!: Property;

  constructor(
    private propertyService: PropertyService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('userId')!;
    this.propertyId = this.route.snapshot.paramMap.get('propertyId')!;
    this.loadProperty();
  }

  loadProperty() {
    this.isLoading = true;

    this.propertyService.getPropertyByPropertyId(this.propertyId).subscribe({
      next: (data) => {
        this.property = data;
        this.isLoading = false;
      },
      error: (error) => {
        this.isLoading = false;
        alert('Error loading property');
      },
    });
  }

  onEditProperty() {
    this.router.navigate([
      `/dashboard/users/${this.userId}/properties/${this.propertyId}/edit`,
    ]);
  }
}
