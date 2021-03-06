import * as course from '../course/store/index';
import * as fromBookmark from './store/index';
import * as fromLearningLayout from '../learning-path/store/index';
import { AutoUnsubscribe } from 'ngx-auto-unsubscribe';
import { BACK_TO_INDEX } from '../course/store/actions/course.actions';
import { GET_BOOK_MARK, GET_BOOK_MARK_BY_USERID } from './store/actions/bookmark.actions';
import { StorageService } from '../../../common/services/storage.service';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs/Subscription';
import { Component, OnInit , OnDestroy} from '@angular/core';


@Component({
  selector: 'ota-bookmarks',
  templateUrl: './bookmarks.component.html',
  styleUrls: ['./bookmarks.component.scss']
})
@AutoUnsubscribe()
export class BookmarksComponent implements OnInit, OnDestroy {
  userId: string;
  bookmark: Subscription;
  courseBookmark: Subscription;
  isCourseDetailPage = false;

  constructor(
    private store: Store<any>,
    private storageService: StorageService) {
      this.userId = this.storageService.getCurrentUserId();
  }

  ngOnInit() {
    this.store.dispatch({ type: BACK_TO_INDEX });
    this.getBookMark();
    this.getBookmarkStatus();
  }

  getBookMark() {
    this.store.dispatch({type: GET_BOOK_MARK, payload: this.userId});
    this.store.select(fromBookmark.selectCourseByBookmarkId).subscribe(res => {
      this.courseBookmark = res;
    });
  }

  getBookmarkStatus() {
    this.store.dispatch({type: GET_BOOK_MARK_BY_USERID, payload: this.userId});
    this.store.select(fromBookmark.selectBookmarkByUserId).subscribe(res => {
      if (res) {
        this.bookmark = res;

        this.courseDetailPage();
      }
    });
  }

  courseDetailPage() {
    this.store.select(course.selectCourseDetailPageState).subscribe(res => {
      res === true ? this.isCourseDetailPage = true : this.isCourseDetailPage = false;
    });
  }

  ngOnDestroy() {

  }
}
