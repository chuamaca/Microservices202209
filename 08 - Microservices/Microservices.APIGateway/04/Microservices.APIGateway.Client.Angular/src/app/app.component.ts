import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { DataService } from './services/data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  token = ""

  private authUrl = "http://localhost:8089/api/auth";

  constructor(private http: HttpClient, private dataService: DataService) {
    this.token = dataService.Token;
  }


  public btnGenerateTokenOnClick() {
    this.http.get<any>(this.authUrl).subscribe(result => {
      this.token = result.data;
      this.dataService.Token = this.token;
    }, error => console.error(error));
  }
}
