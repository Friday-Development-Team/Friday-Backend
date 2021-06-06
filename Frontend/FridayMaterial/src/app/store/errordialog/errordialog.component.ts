import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'friday-errordialog',
  templateUrl: './errordialog.component.html',
  styleUrls: ['./errordialog.component.scss']
})
export class ErrordialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: string) { }

  ngOnInit(): void {
  }

}
