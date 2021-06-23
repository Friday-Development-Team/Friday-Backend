import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'friday-management-base',
  templateUrl: './management-base.component.html',
  styleUrls: ['./management-base.component.scss']
})
export class ManagementBaseComponent implements OnInit {

  private roles: string[] = []

  constructor(private auth: AuthService) {
    this.auth.getRoles().subscribe(s => this.roles = s.map(s => s.toLowerCase()))
  }

  ngOnInit(): void {
  }

  onTabClick() {
    console.log('Tab clicked')
  }

  hasRole(role: string): boolean {
    return true
    return this.roles.includes(role.toLowerCase())
  }

}
