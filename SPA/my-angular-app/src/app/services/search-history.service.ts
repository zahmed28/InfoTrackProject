import { inject,Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { environment } from '../../environments/environment.development'; 


@Injectable({
  providedIn: 'root'
})
export class SearchHistoryService {
  
  private apiUrl = environment.apiUrl;
 
  constructor(private http: HttpClient) { }

  getSearchHistory(): Observable<any> {
    debugger;
    return this.http.get(`${this.apiUrl}/Search/SearchHistory`);
  }
}
