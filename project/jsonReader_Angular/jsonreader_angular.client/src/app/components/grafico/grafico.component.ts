import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { DTE, DTER, Emisor, EmisorR, Receptor, ReceptorR, Resumen, ResumenR } from '../../services/Interfaces/Interfaces';
import { DTESaveService } from '../../services/dteSrv/dtesave.service';
import { Chart, ChartData, ChartType, registerables } from 'chart.js';
import { ChartService } from '../../services/chartSrv/chart.service';
import Swal from 'sweetalert2';



@Component({
  selector: 'app-grafico',
  templateUrl: './grafico.component.html',
  styleUrl: './grafico.component.css'
})
export class GraficoComponent implements OnInit{

  @ViewChild('forChart', { static: false }) forChart!: ElementRef<HTMLCanvasElement>;
  @ViewChild('dropd') dropd!: ElementRef;
  @ViewChild('grafico', { static: false }) grafico!: ElementRef<HTMLDivElement>;
  



  public receivedEmisores: EmisorR[] = [];
  public receivedDtes: DTER[] = [];
  public receivedReceptores: ReceptorR[] = [];
  public receivedResumen: ResumenR[] = [];

  

 tipos = [["01","Factura"],
          ["03","Comprobante de crédito fiscal"],
          ["04","Nota de remisión"],
          ["05","Nota de crédito"],
          ["06","Nota de débito"],
          ["07","Comprobante de retención"],
          ["08","Comprobante de liquidación"],
          ["09","Documento contable de liquidación"],
          ["11","Facturas de exportación"],
          ["14","Factura de sujeto excluido"],
    ["15", "Comprobante de donación"]];

  tipo: string = "";
  lbl: Date[] = [];
  datos: number[] = []; 
  data: number[] = [10, 25, 3, 4, 5]
  graphTitle = "Prueba"
  labels: string[] = ["label1", "label2", "label3", "label4", "label5"]

  constructor(private dteSrv: DTESaveService, private chart: ChartService) { }


  ngOnInit() {
    this.getEmisores();
    this.getReceptores();
    this.getDTE();
    this.getResumen();

  }


  getEmisores() {
    this.dteSrv.getEmisores().subscribe({
      next: (result) => {
        this.receivedEmisores = result.response

        console.log(this.receivedEmisores);
        
      },
      error: (error) => {
        console.log(error);
      }
     
    })
  }

  getReceptores() {
    this.dteSrv.getReceptores().subscribe({
      next: (result) => {
        this.receivedReceptores = result.response

        console.log(this.receivedReceptores);

      },
      error: (error) => {
        console.log(error);
      }

    })
  }

  getDTE() {
    this.dteSrv.getAllDTEs().subscribe({
      next: (result) => {
        this.receivedDtes = result.response;
        console.log(this.receivedDtes);

      },
      error: (error) => {
        console.log(error);
      }

    })
  }

  getResumen() {
    this.dteSrv.getAllDTEs().subscribe({
      next: (result) => {
        this.receivedResumen = result.response

        console.log(this.receivedResumen);

      },
      error: (error) => {
        console.log(error);
      }

    })
  }

  getTipoDTE(txt: string, tipo:string) {
    this.dropd.nativeElement.textContent = txt;
    this.tipo = tipo;    
    console.log(this.tipo);

    this.filterDTEs();
  }

  filterDTEs() {
    const cssStyled = window.getComputedStyle(this.grafico.nativeElement);
    const filtered = this.receivedDtes.filter(dte => dte.tipoDte == this.tipo)
    console.log(filtered.length);
    if (filtered.length == 0) {

      if (cssStyled.display === 'block') {
        console.log("prueba jije")
        this.grafico.nativeElement.style.display = "none"
      }
      Swal.fire({
        title: "DTE inexistentes",
        text: "No existen DTEs en la base de datos",
        icon: "error",
        timer: 1500
      })
    } else {
      
      this.createLbl(filtered);
      this.createData(filtered);
      

      if (cssStyled.display === 'none') {
        console.log("prueba jije")
        this.grafico.nativeElement.style.display = "block";
      }
      this.chart.loadChart(this.graphTitle, 'Creditos Fiscales', this.datos, this.lbl, 'line-chart', 'line');
    }
    
  }

  createLbl(filtered: any) {
    this.lbl = filtered.map((e: DTER) => {return e.fecEmi.toString().split("T")[0] });
    console.log(this.lbl)
  }

  createData(filtered: any) {
    this.datos = filtered.map((e: DTER) => e.resumen?.totalGravada);
    console.log(this.datos)
  }
}
