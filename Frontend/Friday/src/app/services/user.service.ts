import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject, Observable, timer, BehaviorSubject, interval } from 'rxjs';
import { environment } from 'src/environments/environment';
import { switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  clock: Observable<any> = interval(300000)

  user: Subject<ShopUser> = new BehaviorSubject(new ShopUser('Loading ....', 0))

  constructor(private http: HttpClient) {
    this.getUserInformation().subscribe(s => this.user.next(s))
  }

  startUserPolling() {
    if ((!this.clock))
      this.clock = timer(300000)
    this.clock.pipe(switchMap(() => this.getUserInformation())).subscribe(s => {
      console.log("polling")
      this.user.next(s)
    }
    )
  }
  stopUserPolling() {
    this.clock = null;
  }

  getUserInformation(): Observable<ShopUser> {
    return this.http.get<ShopUser>(`${environment.apiUrl}/user`)
  }


}

export class ShopUser {
  constructor(public name: string, public balance: number) { }
}
