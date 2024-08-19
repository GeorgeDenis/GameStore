import { Injectable } from '@angular/core';
import { UserService } from './user.service';
import { IUser } from '../models/user/user';
import { Observable } from 'rxjs';
import { map } from 'rxjs';
import { IPostReview } from '../models/reviews/postReview';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  constructor(private userService: UserService, private baseService: BaseService) { }

  getReviewer(id: string): Observable<IUser> {
    return this.userService.getUserById(id).pipe(
      map(response => response.user)
    )
  }

  postReview(review: IPostReview) {
    return this.baseService.post("/Review", review);
  }
}
