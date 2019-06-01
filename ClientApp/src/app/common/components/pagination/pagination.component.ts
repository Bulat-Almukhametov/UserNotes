import {Component, Input, OnInit} from '@angular/core';
import * as _ from 'underscore';
import {Router} from "@angular/router";

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html'
})
export class PaginationComponent implements OnInit {

  constructor(private router: Router) {}

  ngOnInit() {}

  public pager: Pager;
  private _totalItems = 0;
  @Input() pageSize: number = 5;

  get totalItems():number {
    return this._totalItems;
  }
  @Input() set totalItems(value: number){
    if (value && !this._totalItems){
      this.initPager(this.currentPage, value);
    }
    this._totalItems = value;
  }
  @Input() pagingUrl: string;
  @Input() currentPage: number;

  initPager(page: number, totalItems: number) {
    // calculate total pages
    let totalPages = Math.ceil(totalItems / this.pageSize);

    let startPage: number, endPage: number;


    if (totalPages <= 5) {
      startPage = 1;
      endPage = totalPages;
    } else {
      if (page <= 3) {
        startPage = 1;
        endPage = 5;
      } else if (page + 1 >= totalPages) {
        startPage = totalPages - 4;
        endPage = totalPages;
      } else {
        startPage = page - 2;
        endPage = page + 2;
      }
    }

    // calculate start and end item indexes
    let startIndex = (page - 1) * this.pageSize;
    let endIndex = Math.min(startIndex + this.pageSize - 1, this.totalItems - 1);

    // create an array of pages to ng-repeat in the pager control
    let pages = _.range(startPage, endPage + 1);

    // return object with all pager properties required by the view
    this.pager = {
      totalItems: totalItems,
      currentPage: page,
      pageSize: this.pageSize,
      totalPages: totalPages,
      startPage: startPage,
      endPage: endPage,
      startIndex: startIndex,
      endIndex: endIndex,
      pages: pages
    };
  }

  setPage(page: number) {
    if (this.pager && (page < 1 || page > this.pager.totalPages || this.pager.currentPage == page)) {
      return;
    }
    this.initPager(page, this.totalItems);
    this.router.navigate([this.pagingUrl, page]);
  }

}

interface Pager{
  totalItems: number,
  currentPage: number,
  pageSize: number,
  totalPages: number,
  startPage: number,
  endPage: number,
  startIndex: number,
  endIndex: number,
  pages: string
}
