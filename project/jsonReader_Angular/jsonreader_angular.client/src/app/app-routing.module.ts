import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DTEComponent } from './components/dte/dte.component';
import { GraficoComponent } from './components/grafico/grafico.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'dte',
    pathMatch: 'full',
  },
  {
    path: 'dte',
    component: DTEComponent,
  },
  {
    path: 'grafico',
    component: GraficoComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
