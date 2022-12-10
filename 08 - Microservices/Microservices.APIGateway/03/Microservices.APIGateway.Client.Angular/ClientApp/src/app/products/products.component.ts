import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  public products: Product[];
  private productUrl = "https://localhost:44345/gateway/products";

  constructor(http: HttpClient) {
    http.get<Product[]>(this.productUrl, {
      headers: {
        'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJFcmljayIsInVuaXF1ZV9uYW1lIjoiRXJpY2siLCJyb2xlIjpbIlNBTEVTTUFOIiwiVVNFUiJdLCJ1c2VyVHlwZSI6IlNBTEVTTUFOIiwibmJmIjoxNjcwNjk2Mzk0LCJleHAiOjE2NzEzMDExOTMsImlhdCI6MTY3MDY5NjM5NH0.zDvr-8eTb_81YZ1LqRZGolZBC3O_2nKNaJSQU-04fZE'
      }
    }).subscribe(result => {
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
