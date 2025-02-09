import { Component, OnInit } from '@angular/core';
import { MockDataService } from '../../core/services/mock-data.service';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-preview',
  standalone: true,
  imports: [JsonPipe],
  templateUrl: './preview.component.html',
  styleUrl: './preview.component.css',
})
export class PreviewComponent  implements OnInit {
  mockJsonData: any;

  constructor(private mockDataService: MockDataService) {
  }

  ngOnInit() {
    this.mockDataService.getmockJsonData$.subscribe((data) => {
      this.mockJsonData = data;
    });
  }
}
