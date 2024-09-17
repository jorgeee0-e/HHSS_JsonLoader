import { Component, ViewChild, ElementRef, viewChild, OnInit } from '@angular/core'; /*import viewchild y elementref para acceder al DOM*/
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import Swal from 'sweetalert2';
import { DTE, DTER, Emisor, EmisorR, Receptor, ReceptorR, Resumen, ResumenR } from '../../services/Interfaces/Interfaces';
import { lastValueFrom } from 'rxjs';
import { DTESaveService } from '../../services/dteSrv/dtesave.service';

@Component({
  selector: 'app-dte',
  templateUrl: './dte.component.html',
  styleUrl: './dte.component.css'
})
export class DTEComponent implements OnInit {

  /*viewchild para acceder a los elemtos del DOM*/
  @ViewChild('fileInput', { static: false }) fileInput!: ElementRef<HTMLInputElement>;
  @ViewChild('fileList', { static: false }) fileList!: ElementRef<HTMLUListElement>;
  @ViewChild('loadButton', { static: false }) loadButton!: ElementRef<HTMLButtonElement>;

  public emisores: Emisor[] = [];
  public dtes: DTE[] = [];
  public receptores: Receptor[] = [];
  public resumen: Resumen[] = [];

  public receivedEmisores: EmisorR[] = [];
  public receivedDtes: DTER[] = [];
  public receivedReceptores: ReceptorR[] = [];
  public receivedResumen: ResumenR[] = [];
  constructor(private dteSrv: DTESaveService) { }

  files: { fileName: string, content: string, expanded: boolean }[] = [];

  ngOnInit() {
    /*this.retrieveFiles();*/
/*    this.validateReceptores();*/
  }

  /*funcion para abrir el input dialog*/
  openFileDialog() {
    this.fileInput.nativeElement.click();
  }

  /*funcion para manejar los archivos seleccionados*/
  handleSelectedFiles(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      const files = Array.from(input.files); //array con el resultado de los files seleccionados
      files.forEach(file => { //lector de cada file en el array
        const reader = new FileReader; //variable reader
        reader.readAsText(file);
        reader.onload = () => {
          console.log(file)
          console.log(file.name)
          this.displayFile(file, reader.result as string);
          this.saveFiles(file.name, reader.result as string);
        }     
      }) 
    }
  }

  displayFile(file:File, content:string) {
    const itemList = document.createElement('li');
    itemList.className = 'item';

    const fileDetails = document.createElement('span'); /*creando elemento para mostrar el nombre del archivo*/
    fileDetails.className = 'file-details';
    fileDetails.textContent = `${file.name} (${(file.size / 1024).toFixed(2)} KB)`;
    itemList.appendChild(fileDetails);

    const deleteIcon = document.createElement('i') /*creando icono para borrar*/
    deleteIcon.className = 'fa-solid fa-trash' 
    deleteIcon.style.cursor = 'pointer';
    deleteIcon.style.fontSize = '20px'
    deleteIcon.style.marginLeft = '5px'
    deleteIcon.onclick = () => {
      itemList.remove();
    };
    itemList.appendChild(deleteIcon);

    this.fileList.nativeElement.appendChild(itemList);
    console.log(localStorage);
  }

  clearMain() {
    const itemList = document.getElementsByClassName('item');
    const itemsListA = Array.from(itemList);
    itemsListA.forEach(item => {
      item.parentNode?.removeChild(item);
    })
    
  }

  /*guardar archivos json en localStorage*/
  saveFiles(fileName: string, content: string) {
    let files = JSON.parse(localStorage.getItem('jsonFiles') || '[]');
    console.log("Files en local storage: ")
    console.log(files);
    files.push({ fileName, content });
    localStorage.setItem('jsonFiles', JSON.stringify(files));
    
  }


  loadFiles(event: Event) {
    let files = JSON.parse(localStorage.getItem('jsonFiles') || '[]');

    if (files.length === 0) {
      Swal.fire({
        title: 'Warning',
        text: 'No loaded files',
        icon: 'warning',
        customClass: {
          confirmButton: 'btn btn-primary px-4',
        },
        buttonsStyling: false,
      });

    } else {
      this.retrieveFiles();
    }
  }

  retrieveFiles(): void /*{filename: string,content: string} []*/ {
    const files = JSON.parse(localStorage.getItem('jsonFiles') || '[]')
    
    this.files = (Array.isArray(files) ? files : []).map(file => (

      {
      fileName: file.fileName,
      content: JSON.parse(file.content),      
      expanded: false
      }
    ));
/*    this.showConsole(files);*/
    this.guardarDTE(files);

    
  }

  toggleContent(file: { fileName: string, content: string, expanded: boolean }): void { //para hacer expandible las celdas de la tabla
    file.expanded = !file.expanded;
  }


  async guardarDTE(files: any) {
    const arrFiles = (Array.isArray(files) ? files : []);
    let allContent: any[]= [];
    arrFiles.forEach(file => {
      allContent.push(JSON.parse(file.content));
    });

    await this.validateAll(allContent)
    await this.sendAll();
    localStorage.clear();
    this.clearMain();
    
/*    console.log(allContent)*/
    
  }

  showConsole(files:any) {
    const filesArray = Array.from(Array.isArray(files) ? files : []).map(file => ({
      fileName: file.fileName,
      content: JSON.parse(file.content),
      expanded: false

    }));

    filesArray.forEach(file => {
      console.log(file.content.identificacion.codigoGeneracion);
      console.log(file.content.identificacion.numeroControl);
      console.log(file.content.identificacion.fecEmi);
    })
  }

  async validateAll(allContent: any[]): Promise<void> {
    await this.createEmisorList(allContent);
    await this.validateEmisores()

    await this.createReceptorList(allContent)
    await this.validateReceptores();

    await this.createDTEList(allContent)
    await this.validateDte();

    await this.createResumenList(allContent)
    await this.validateResumen();

  }


  //Crea un array con todos los objetos de tipo DTE que se enviaran
  createDTEList(allContent: any[]): Promise<void> {
    return new Promise((resolve) => {
      this.dtes = allContent.map(parsed => {
        const tipo = parsed.identificacion.tipoDte;
        return {
          CodigoGeneracion: parsed.identificacion.codigoGeneracion || '',
          Nit_emisor: parsed.emisor.nit || '',
          Nit_receptor: tipo == "01" ? (parsed.receptor.numDocumento || '') : (parsed.receptor.nit || ''),
          Version: parsed.identificacion.version || 0,
          TipoDte: parsed.identificacion.tipoDte || '',
          NumeroControl: parsed.identificacion.numeroControl || '',
          FecEmi: parsed.identificacion.fecEmi || '',
          resumen: parsed.resumen || '',
        } as DTE;
      });
      resolve();
    })

  }
  //Crea un array con todos los objetos de tipo Resumen que se enviaran
  createResumenList(allContent: any[]): Promise<void> {
    return new Promise((resolve) => {
      this.resumen = allContent.map(parsed => {
        return {
          CodigoGeneracion: parsed.identificacion.codigoGeneracion || '',
          TotalNoSuj: parsed.resumen.totalNoSuj || 0.00,
          TotalExenta: parsed.resumen.totalExenta || 0.00,
          TotalGravada: parsed.resumen.totalGravada || 0.00,
          SubTotalVentas: parsed.resumen.subTotalVentas || 0.00,
          DescuExenta: parsed.resumen.descuExenta || 0.00,
          DescuGravada: parsed.resumen.descuGravada || 0.00,
          PorcentajeDescuento: parsed.resumen.porcentajeDescuento || 0.00,
          TotalDescu: parsed.resumen.totalDescu || 0.00,
          IvaPerci1: parsed.resumen.ivaPerci || 0.00,
          IvaRete1: parsed.resumen.ivaRete1 || 0.00,
          MontoTotalOperacion: parsed.resumen.montoTotalOperacion || 0.00,
          TotalNoGravado: parsed.resumen.totalNoGravado || 0.00,
          TotalPagar: parsed.resumen.totalPagar || 0.00,
          TotalLetras: parsed.resumen.totalLetras || 0.00,
          SaldoFavor: parsed.resumen.saldoFavor || 0.00,
        } as Resumen
      });
      console.log("Lista de resumenes a enviar");
      console.log(this.resumen)
      resolve();
    })
  }
  //Crea un array con todos los objetos de tipo Emisor que se enviaran
  createEmisorList(allContent: any[]): Promise<void> {
    return new Promise((resolve) => {

      this.emisores = allContent.map(parsed => {
        const tipo = parsed.identificacion.tipoDte;
        return {
          Nit: tipo =='15'? (''): (parsed.emisor.nit || ''),
          Nombre: tipo == '15' ? ('') : (parsed.emisor.nombre || ''),
          CodActividad: tipo == '15' ? ('') : (parsed.emisor.codActividad || ''),
          Departamento: tipo == '15' ? ('') : (parsed.emisor.direccion.departamento || ''),
          Municipio: tipo == '15' ? ('') : (parsed.emisor.direccion.municipio || ''),
          Complemento: tipo == '15' ? ('') : (parsed.emisor.direccion.complemento || ''),
          Correo: tipo == '15' ? ('') : (parsed.emisor.correo || ''),
        } as Emisor
      })
      console.log("Lista de emisores");
      console.log(this.emisores)
      resolve();
})

  }
  //Crea un array con todos los objetos de tipo Receptor que se enviaran
  createReceptorList(allContent: any[]):Promise<void> {
    return new Promise((resolve) => {
      this.receptores = allContent.map(parsed => {
        const tipo = parsed.identificacion.tipoDte;
        return {
          Nit: tipo == "01" ? (parsed.receptor.numDocumento) : (parsed.receptor.nit || ''),
          Nombre: parsed.receptor.nombre || '',
          CodActividad: tipo=="11"?(''): (parsed.receptor.codActividad || ''),
          Departamento: tipo =="11"?(''): (parsed.receptor.direccion.departamento || ''),
          Municipio: tipo == "11" ? (''): (parsed.receptor.direccion.municipio || ''),
          Complemento: tipo == "11" ? (parsed.receptor.complemento): (parsed.receptor.direccion.complemento || ''),
          Correo: parsed.receptor.correo || '',
          TipoDocumento: parsed.receptor.tipoDocumento || ''
        } as Receptor
      });
      resolve();
    })
  }

  async validateReceptores() {
    return new Promise<void>((resolve) => {
      this.dteSrv.getReceptores().subscribe({
        next: (result) => {
          this.receivedReceptores = result.response;
          this.receptores = this.receptores.filter(receptor => {
            const existe = this.receivedReceptores.some(recibido => {
              console.log(`Comparando receptor a enviar ${receptor.Nit} y recibido nit recibido ${recibido.nit}`)
              return recibido.nit === receptor.Nit
            })
            return !existe;
          });
          console.log(this.receptores)
          if (!(this.receptores.length > 0)) {
            console.log("No existen nuevos datos de receptores a enviar");
          }
          resolve();

        },
        error: (error) => {
          console.log(error);
          resolve();
        }

      })
    })
    

  }

  validateEmisores() {
    this.dteSrv.getEmisores().subscribe({
      next: (result) => {
        this.receivedEmisores = result.response;
        this.emisores = this.emisores.filter(emisor => { 
          const exist = this.receivedEmisores.some(recibido => {
            console.log(`Comparando emisor a enviar ${emisor.Nit} y recibido nit recibido ${recibido.nit}`)
            return recibido.nit === emisor.Nit
          });
          return !exist;
        });
        console.log("Despues de validar emisores");
        console.log(this.emisores);
        if (!(this.emisores.length > 0)) {
          
          console.log("No existen nuevos datos de emisores a enviar");
        }
      }
    })
  }

  validateDte() {
    this.dteSrv.getAllDTEs().subscribe({
      next: (result) => {
        this.receivedDtes = result.response;
        if (this.receivedDtes.length>0) {
          const nuevoA = this.dtes.filter(dte => {
            
          const existe =  this.receivedDtes.some(recibido => {
              console.log(`Comparando dte a enviar ${dte.CodigoGeneracion} y recibido nit recibido ${recibido.codigoGeneracion}`)
              return recibido.codigoGeneracion === dte.CodigoGeneracion
          })
            return !existe;
          });
          this.dtes = nuevoA;
          console.log(this.dtes)
        }

        if (!(this.dtes.length > 0)) {
          console.log("No existen nuevos datos de DTE a enviar");
        };
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  validateResumen() {
    this.dteSrv.getResumen().subscribe({
      next: (result) => {
        this.receivedResumen = result.response;
        if (this.receivedResumen.length > 0) {
          this.resumen = this.resumen.filter(resumen => {
            const existe = this.receivedResumen.some(recibido => {
              console.log(`Comparando resumen a enviar ${resumen.CodigoGeneracion} y recibido resumen recibido ${recibido.codigoGeneracion}`)
              return recibido.codigoGeneracion === resumen.CodigoGeneracion
            })
            return !existe;
          });
          console.log(this.resumen)
        }
        if (!(this.resumen.length > 0)) {
          console.log("No existen nuevos datos de resumen a enviar")
        };
      },
      error: (error) => {
        console.log(error)
      }
    })
  }

  async sendEmisores(): Promise<void> {
    console.log("Emisores Por enviar");
    console.log(this.emisores);
    try {
      if (this.emisores.length > 0) {
        const savePromises = this.emisores.map(emisor => {
          return lastValueFrom(this.dteSrv.saveEmisor(emisor));
        });
        const result = await Promise.all(savePromises);
        console.log("Emisores enviados exitosamente", result);
      }
      
    } catch (error) {
      console.error("Error enviando emisores",error)
    }      
  }

  async sendReceptores(): Promise<void> {
    console.log("DTEs por enviar");
    console.log(this.receptores);
    try {
      const savePromises = this.receptores.map(receptor => {
        return lastValueFrom(this.dteSrv.saveReceptor(receptor));
      });
      const result = await Promise.all(savePromises);
      console.log("Receptores enviados exitosamente",result)
    } catch (error) {
      console.error("Error enviando receptores", error)
    }
  }

  async sendDTEs(): Promise<void> {
    console.log("DTEs por enviar");
    console.log(this.dtes);
    try {
      const savePromises = this.dtes.map(dte => {
        return lastValueFrom(this.dteSrv.saveDTE(dte))
      });
      const result = await Promise.all(savePromises);
      console.log("DTEs enviados exitosamente",result)
      //if (result) {
      //  await this.sendResumen();
      //}
      console.log("DTEs enviados exitosamente",result)
    } catch (error) {
      console.error("Error enviando DTEs",error)
    }
  }

  async sendAll(): Promise<void> {
    try {
      await this.sendEmisores();
      await this.sendReceptores();
      await this.sendDTEs();


      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500
      });

    } catch (error) {
      console.error("Error enviando resumen", error)
    }
  }
  }



