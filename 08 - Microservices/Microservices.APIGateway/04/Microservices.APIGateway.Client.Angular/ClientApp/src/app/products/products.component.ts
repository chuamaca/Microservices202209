import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  public products: Product[];
  //private productUrl = "http://localhost:1042/gateway/products";
  private productUrl = "https://api-management-microservices.azure-api.net/gateway/products";

  constructor(http: HttpClient) {
    //http.get<Product[]>(this.productUrl, {
    //  headers: {
    //    'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJFcmljayIsInVuaXF1ZV9uYW1lIjoiRXJpY2siLCJyb2xlIjpbIlNBTEVTTUFOIiwiVVNFUiJdLCJ1c2VyVHlwZSI6IlNBTEVTTUFOIiwibmJmIjoxNjI1Mjk4NzU4LCJleHAiOjE2MjU5MDM1NTgsImlhdCI6MTYyNTI5ODc1OH0.-hfGz7F8jgSHgthlR237FdW3IASIxa-TisW_2TDoenA'
    //  }
    //}).subscribe(result => {
    //  this.products = result;
    //}, error => console.error(error));

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
