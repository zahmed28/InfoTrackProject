import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development'; 

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  //private apiUrl = 'https://localhost:7115/api/Search'; // Update with your real API URL
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getSearchResults(keyword: string, url: string): Observable<any> {
    const requestBody = { keyword, url }; // Construct the request body
    return this.http.post<any>(`${this.apiUrl}/Search`, requestBody);
  }
}
