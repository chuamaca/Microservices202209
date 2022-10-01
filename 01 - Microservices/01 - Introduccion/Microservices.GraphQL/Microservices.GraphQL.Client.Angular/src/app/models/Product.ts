import { ProductReview } from "./ProductReview";

export class Product {
  constructor() {
    this.id = 0;
    this.name = '';
    this.type = '';
    this.description = '';
    this.price = 0;
    this.stock = 0;
    this.rating = 0;
    this.introducedAt = new Date();
    this.photoFileName = '';
    this.productReviews = [];
  }
  public id: number;
  public name: string
  public type: string;
  public description: string;
  public price: number;
  public stock: number;
  public rating: number;
  public introducedAt: Date;
  public photoFileName: string;
  public productReviews : ProductReview[];
}
