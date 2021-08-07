import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../store/dialog/dialog.component';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

  constructor(private dialog: MatDialog) { }

  startSpinner(): void {
    this.dialog.open(DialogComponent)
  }

  stopSpinner(delay: number): void {
    setTimeout(() => this.dialog.closeAll(), delay)
  }
}
