import { Component, OnInit, Inject } from '@angular/core';
import { Product } from '../models/Product';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data-graphql',
  templateUrl: './fetch-data-graphql.component.html',
  styleUrls: ['./fetch-data-graphql.component.css']
})
export class FetchDataGraphqlComponent implements OnInit {

  products?: Product[];
  urlGraphQL = "https://localhost:7024/graphql/";
  
  constructor(http: HttpClient) {
    const query = this.getQuery();
    http.post<any>(this.urlGraphQL, { query: query }).subscribe(result => {
      this.products = result.data.products;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  private getQuery(): string {
    return `
      {
        products(order: {type:ASC}){
          id,
          description,
          introducedAt,
          name,
          photoFileName,
          price,
          rating,
          stock,
          type,    
          productReviews {
            id,
            title,
            review
          }
        }
      }`;
  }

}
