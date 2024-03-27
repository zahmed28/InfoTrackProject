using APIGateway.Application.Queries;
using APIGateway.Controllers;
using APIGateway.Domain.Entities;
using InfoTrack.Services.Tests.Mock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTrack.Services.Tests
{
    public class SearchControllerTests
    {
        private readonly ManualMockMediator _mediatorMock;
        private readonly ManualMockLogger<SearchController> _loggerMock;
        private readonly SearchController _searchController;
        public SearchControllerTests()
        {
            _mediatorMock = new ManualMockMediator();
            _loggerMock = new ManualMockLogger<SearchController>();
            _searchController = new SearchController(_loggerMock, _mediatorMock);
        }

        [Fact]
        public async Task Search_WhenGivenValidQuery_ShouldReturnOkResultWithCorrectRankingPositions()
        {
            // Arrange
            var query = new GetSearchQuery { Keyword = "infotrack", URL = "infotrack.co.uk" };
            var expectedResult = new List<int> { 1 };
            _mediatorMock.Response = new Ranking
            {
                RankingPosition = new List<int> { 1 }
            };

            // Act
            var result = await _searchController.Search(query);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Ranking>(okResult.Value);
            Assert.Equal(expectedResult, returnValue.RankingPosition);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WhenSearchHistoyAreFound()
        {
            var query = new GetSearchHistoryQuery();
            _mediatorMock.Response = new PagedResult<SearchResults>
            {
                Records = new List<SearchResults>
                {
             new SearchResults { Id = 1, Query = "Test1", ResultURL = "www.tmgroup.co.uk", DateCreated = DateTime.Now },
             new SearchResults { Id = 2, Query = "Test2", ResultURL = "www.infotrack.co.uk", DateCreated = DateTime.Now }
        },

            };

            var result = await _searchController.SearchHistory(query);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PagedResult<SearchResults>>(okResult.Value);
            Assert.Equal(2, returnValue.Records.Count());
        }        
    }
}
