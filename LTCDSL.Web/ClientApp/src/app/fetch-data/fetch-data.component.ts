import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  public categories: any=[];
  public products: any=[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<WeatherForecast[]>('http://localhost:5000/' + 'weatherforecast').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));

    http.post<Categories>('http://localhost:5000/' + 'api/Categories/get-all-categories', null).subscribe(result => {
      this.categories = result.data;
    }, error => console.error(error));

    http.post<Products>('http://localhost:5000/' + 'api/Products/get-all-products', null).subscribe(result => {
      this.products = result.data;
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface Categories {
  data: Object;
}

interface Products {
  data: Object;
}