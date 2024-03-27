import { Component } from '@angular/core';
import { SearchService } from '../services/search.service'; 

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  searchKeyword: string = '';
  searchUrl: string = '';
  displayText: string | null = null;

  constructor(private searchService: SearchService) { }

  onSearch(): void {
    this.searchService.getSearchResults(this.searchKeyword, this.searchUrl).subscribe({
      next: (data) => {
        debugger;
        // Check if data and data.rankingPosition are valid and not empty
        if (data && Array.isArray(data.rankingPosition) && data.rankingPosition.length > 0) {
          // Join the array of numbers into a string, separated by commas
          const joinedString = data.rankingPosition.join(", ");
          // Update displayText to show where the keyword appears
          this.displayText = `This keyword appears in positions: ${joinedString}.`;
        } else {
          // Update displayText if the keyword does not appear or if there's no valid data
          this.displayText = 'This keyword does not appear in the provided URL.';
        }
      },
      error: (error) => {        
        console.error('Search API error:', error);
        this.displayText = 'An error occurred during the search.';
      }
    });
  }
}
