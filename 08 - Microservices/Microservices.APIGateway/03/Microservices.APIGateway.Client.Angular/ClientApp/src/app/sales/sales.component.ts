import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.css']
})
export class SalesComponent implements OnInit {

  public orders: Order[];
  private salesUrl = "https://localhost:44345/gateway/sales";

  constructor(http: HttpClient) {
    http.get<Order[]>(this.salesUrl, {
      headers: {
        'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJFcmljayIsInVuaXF1ZV9uYW1lIjoiRXJpY2siLCJyb2xlIjpbIlNBTEVTTUFOIiwiVVNFUiJdLCJ1c2VyVHlwZSI6IlNBTEVTTUFOIiwibmJmIjoxNjI1Mjk4NzU4LCJleHAiOjE2MjU5MDM1NTgsImlhdCI6MTYyNTI5ODc1OH0.-hfGz7F8jgSHgthlR237FdW3IASIxa-TisW_2TDoenA'
      }
    }).subscribe(result => {
      this.orders = result;
    }, error => console.error(error));
  }
  ngOnInit() {
  }

}

interface Order {
  OrderId: number;
  Description: string;
  Items: OrderItem[];
  Created: Date;
  Updated: Date;
}

interface OrderItem {
  OrderId: number;
  ProductId: number;
  Created: Date;
  Updated: Date;
}
