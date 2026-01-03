export interface Property {
  id: string;
  userId: string;

  title: string;
  price: number;
  city: string;
  state: string;
  locality: string;
  pincode: string;
  noOfRooms: number;
  carpetAreaSqft: number;
  builtYear: number;
  balcony: boolean;
  parking: boolean;

  propertyImageUrl?: string;
  propertyImagePublicId?: string;
  hallImageUrl?: string;
  hallImagePublicId?: string;
  kitchenImageUrl?: string;
  kitchenImagePublicId?: string;
  bathroomImageUrl?: string;
  bathroomImagePublicId?: string;
  bedroomImageUrl?: string;
  bedroomImagePublicId?: string;
  parkingImageUrl?: string;
  parkingImagePublicId?: string;
}
