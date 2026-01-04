import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Property } from 'src/app/core/models/property.model';
import { PropertyService } from 'src/app/core/services/property.service';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css'],
})
export class PropertyListComponent implements OnInit {
  userId!: string;
  property: Property[] = [];
  isLoading: boolean = false;

  constructor(
    private propertyService: PropertyService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('userId') || '';
    this.loadProperties();
  }

  loadProperties() {
    this.isLoading = true;

    this.propertyService.getPropertyByUserId(this.userId).subscribe({
      next: (data) => {
        this.property = data.filter((p) => p.userId === this.userId);
        this.isLoading = false;
      },
      error: (error) => {
        this.isLoading = false;
        alert('Error loading properties');
      },
    });
  }

  onCreateProperty() {
    this.router.navigate([`dashboard/users/${this.userId}/properties/create`]);
  }

  onViewProperty(propertyId: string) {
    this.router.navigate([
      `dashboard/users/${this.userId}/properties/${propertyId}`,
    ]);
  }

  onDeleteProperty(propertyId: string) {
    if (!confirm('Are you sure you want to delete this property?')) {
      return;
    }

    this.propertyService.deletePropertyByPropertyId(propertyId).subscribe({
      next: () => {
        this.property = this.property.filter((p) => p.id !== propertyId);
      },
      error: () => {
        alert('Error deleting property');
      },
    });
  }
}
