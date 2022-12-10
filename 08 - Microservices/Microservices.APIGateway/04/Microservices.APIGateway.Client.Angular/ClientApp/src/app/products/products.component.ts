import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  public products: Product[];
  private productUrl = "http://localhost:1042/gateway/products";
  //private productUrl = "https://api-management-microservices.azure-api.net/gateway/products";

  constructor(http: HttpClient) {
    http.get<Product[]>(this.productUrl, {
      headers: {
        'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJFcmljayIsInVuaXF1ZV9uYW1lIjoiRXJpY2siLCJyb2xlIjpbIlNBTEVTTUFOIiwiVVNFUiJdLCJ1c2VyVHlwZSI6IlNBTEVTTUFOIiwibmJmIjoxNjcwNjk3NDkwLCJleHAiOjE2NzEzMDIyOTAsImlhdCI6MTY3MDY5NzQ5MH0.xaGDqxjrB7V9Rujoj0I0ycTBTlagfPCwmy2QpGMJl8Q'
      }
    }).subscribe(result => {
      this.products = result;
    }, error => console.error(error));

    http.get<Product[]>(this.productUrl).subscribe(result => {
      this.products = result;
    }, error => console.error(error));
  }
  ngOnInit() {
  }

}


interface Product {
  ProductId: number;
  Description: string;
  Price: number;
  Created: Date;
  Updated: Date;
}
