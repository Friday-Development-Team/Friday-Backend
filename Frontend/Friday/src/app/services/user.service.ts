import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject, Observable, timer, BehaviorSubject, interval } from 'rxjs';
import { environment } from 'src/environments/environment';
import { switchMap } from 'rxjs/operators';
import { DataService } from './data.service';
import { RefreshService } from './refresh.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  clock: Observable<any> = interval(300000)

  user: Subject<ShopUser> = new BehaviorSubject(new ShopUser('Loading ....', 0))

  constructor(private http: HttpClient, private refresh: RefreshService) {
    //Gets the first user information when this service is loaded in. Polling is started on component side to allow component to load
    this.getUserInformation().subscribe(s => this.user.next(s))
    //Triggers an instant refresh of information
    this.refresh.refresh.subscribe(s => this.getUserInformation().subscribe(t => this.user.next(t)))
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
