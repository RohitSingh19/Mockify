import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogActions, MatDialogContent } from '@angular/material/dialog';


@Component({
  selector: 'app-custom-category',
  standalone: true,
  imports: [MatDialogActions, MatDialogContent],
  templateUrl: './custom-category.component.html',
  styleUrl: './custom-category.component.css'
})
export class CustomCategoryComponent {
  constructor(
    public dialogRef: MatDialogRef<CustomCategoryComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  closeDialog(): void {
    this.dialogRef.close();
  }
}






