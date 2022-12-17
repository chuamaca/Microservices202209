import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.css']
})
export class SalesComponent implements OnInit {

  public orders!: Order[];
  private salesUrl = "http://localhost:8087/gateway/sales";
  
  constructor(private http: HttpClient, private dataService: DataService) {
    this.LoadSales();
  }

  ngOnInit() {
  }

  private LoadSales() {
    this.http.get<Order[]>(this.salesUrl, {
      headers: {
        'Authorization': `Bearer ${this.dataService.Token}`
      }
    }).subscribe(result => {
      this.orders = result;
    }, error => console.error(error));
  }

  public btnLoadSalesOnClick() {
    this.LoadSales();
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
