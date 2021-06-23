import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MessageDialogComponent } from '../shared/messagedialog/messagedialog.component';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private dialog: MatDialog) { }

  /**
   * Opens a dialog and automatically closes it after a given amount of time.
   * @param timeout Time in milliseconds after which the dialog will autoclose. Set to -1 to not close automatically.
   * @param data Text to display
   */
  openTimedDialog(timeout: number, data?: string) {
    if (timeout < 0 && timeout != -1)
      return

    this.dialog.open(MessageDialogComponent, { data })

    if (timeout != -1)
      setTimeout(() => this.dialog.closeAll(), timeout)
  }

  displayErrorMessage(err: string) {
    this.openTimedDialog(-1, err)
  }
}
