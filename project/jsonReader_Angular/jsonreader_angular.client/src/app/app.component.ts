import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';


interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {


  @ViewChild('optionsList') optionsList!: ElementRef<HTMLDivElement>;

  constructor(private http: HttpClient ) {}

  ngOnInit() {
  }
  ngAfterViewInit() {
    // Puedes acceder a optionsList aquí porque está garantizado que está inicializado
  }


  toggleMenu() {
    const menu = this.optionsList.nativeElement;
    if (menu.classList.contains('show')) {
      menu.classList.remove('show');
    } else {
      menu.classList.add('show');
    }
  }

  title = 'jsonreader_angular.client';
}
