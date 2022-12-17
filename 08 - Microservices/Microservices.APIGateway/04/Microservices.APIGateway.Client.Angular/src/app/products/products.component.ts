import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  public products!: Product[];
  private productUrl = "http://localhost:8087/gateway/products";

  constructor(private http: HttpClient, private dataService: DataService) {
    this.LoadProducts();
  }

  ngOnInit() {
  }

  private LoadProducts() {
    this.http.get<Product[]>(this.productUrl, {
      headers: {
        'Authorization': `Bearer ${this.dataService.Token}`
      }
    }).subscribe(result => {
      this.products = result;
    }, error => console.error(error));    
  }

  public btnLoadProductsOnClick() {
    this.LoadProducts();
  }
}


interface Product {
  ProductId: number;
  Description: string;
  Price: number;
  Created: Date;
  Updated: Date;
}
