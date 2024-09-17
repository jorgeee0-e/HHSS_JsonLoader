import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DTEComponent } from './components/dte/dte.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { DTESaveService } from './services/dteSrv/dtesave.service';
import { CommonModule } from '@angular/common';
import { GraficoComponent } from './components/grafico/grafico.component';
import { provideCharts } from 'ng2-charts'



@NgModule({
  declarations: [
     AppComponent,
    DTEComponent,
    GraficoComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        FontAwesomeModule,
        CommonModule,
                
    ],
    providers: [
        provideHttpClient(withInterceptorsFromDi()),DTESaveService
    ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
