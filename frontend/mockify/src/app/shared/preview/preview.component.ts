import { Component, OnInit } from '@angular/core';
import { MockDataService } from '../../core/services/mock-data.service';
import { JsonPipe } from '@angular/common';
import { ClipboardModule, Clipboard } from '@angular/cdk/clipboard';
import {MatIconModule} from '@angular/material/icon';
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-preview',
  standalone: true,
  imports: [JsonPipe, ClipboardModule, MatIconModule],
  templateUrl: './preview.component.html',
  styleUrl: './preview.component.css',
})
export class PreviewComponent  implements OnInit {
  mockJsonData: any;

  constructor(private mockDataService: MockDataService, 
              private clipboard: Clipboard, private snackBar: MatSnackBar) {
  }

  ngOnInit() {
    this.mockDataService.getmockJsonData$.subscribe((data) => {
      this.mockJsonData = data;
    });
  }

  copyToClipboard() {
    this.clipboard.copy(JSON.stringify(this.mockJsonData));
    this.snackBar.open("JSON copied to clipboard", "Dismiss", {
      duration: 2000,
    });
  }

  downloadJson() {
    const jsonString = JSON.stringify(this.mockJsonData, null, 2);
    const blob = new Blob([jsonString], { type: "application/json" });
    const url = URL.createObjectURL(blob);
    
    const a = document.createElement("a");
    a.href = url;
    a.download = "mockify-mock-data.json";
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
  }
}
