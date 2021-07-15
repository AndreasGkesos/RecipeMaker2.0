import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiCallService {

  starwarsBase = 'https://localhost:4101/api';

  constructor(private httpClient: HttpClient) { }

  public getAllSuppliers() {
    return this.httpClient.get(`${this.starwarsBase}/supplier/all`)
          .pipe(map((res: any) => res))
  }
}
