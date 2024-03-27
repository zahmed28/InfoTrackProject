import { Component, OnInit } from '@angular/core';
import { SearchHistoryService } from '../services/search-history.service';

@Component({
  selector: 'app-search-history',
  templateUrl: './search-history.component.html',
  styleUrls: ['./search-history.component.css']
})
export class SearchHistoryComponent implements OnInit {

  searchData: any = { records: [] };
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 10; 
  constructor(private searchHistoryService: SearchHistoryService) { }

  ngOnInit(): void {
    this.fetchSearchHistory();
  }

  fetchSearchHistory(): void {
    debugger;
    this.searchHistoryService.getSearchHistory().subscribe({
      next: (data) => {
        debugger;
        //this.searchData = data;
        const start = (this.currentPage - 1) * this.itemsPerPage;
        const end = start + this.itemsPerPage;
        this.searchData = { ...data, records: data.records.slice(start, end) }
      },
      error: (error) => {
        console.error('There was an error!', error);
      }
    });
  }

  
  changePage(newPage: number): void {
    this.currentPage = newPage;
    this.fetchSearchHistory(); 
  }

  get filteredRecords() {
    if (!this.searchData || !this.searchData.records || !this.searchTerm) {
      return this.searchData.records || [];
    }
    return this.searchData.records.filter((record: any) => 
      (record.query && record.query.toLowerCase().includes(this.searchTerm.toLowerCase())) ||
      (record.resultURL && record.resultURL.toLowerCase().includes(this.searchTerm.toLowerCase())) ||
      (record.rankingIndices && record.rankingIndices.toString().includes(this.searchTerm)) ||
      (record.dateCreated && new Date(record.dateCreated).toLocaleDateString().includes(this.searchTerm))
    );
  }
  
}

