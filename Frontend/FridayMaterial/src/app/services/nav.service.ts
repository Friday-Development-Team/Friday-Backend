import { Injectable } from '@angular/core';
import { NavigationEnd, Router, RouterEvent } from '@angular/router';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NavService {

  private current: Subject<string>=new BehaviorSubject<string>("")

  constructor(private router: Router) {
    
    this.router.events.subscribe(s=> {
      if(s instanceof NavigationEnd){
        let url = s.urlAfterRedirects.toLowerCase()
        if(!url.includes("/main/")) return;
        let last = url.split('/').pop()
        console.log(last);
        this.current.next(last)
      }

    })
   }

   getCurrentNavPage(): Observable<string>{
      return this.current
   }

}

