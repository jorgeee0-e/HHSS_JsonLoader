export interface DTE {
  CodigoGeneracion: string
  Nit_emisor: string,
  Nit_receptor: string,
  Version: number,
  TipoDte: string,
  NumeroControl: string,
  FecEmi: Date,  
  resumen?: Resumen,
  
}

export interface DTER {
  codigoGeneracion: string
  nit_emisor: string,
  nit_receptor: string,
  version: number,
  tipoDte: string,
  numeroControl: string,
  fecEmi: Date,
  resumen?: ResumenR,

}

export interface Resumen {
  CodigoGeneracion: string,
  TotalNoSuj: number,
  TotalExenta: number
  TotalGravada: number,
  SubTotalVentas: number,
  DescuExenta: number,
  DescuGravada: number,
  PorcentajeDescuento: number,
  TotalDescu: number,
  IvaPerci1: number,
  IvaRete1: number,
  MontoTotalOperacion: number,
  TotalNoGravado: number,
  TotalPagar: number,
  TotalLetras: number,
  SaldoFavor: number,
  dte?: DTE
}

export interface ResumenR {
  codigoGeneracion: string,
  totalNoSuj: number,
  totalExenta: number
  totalGravada: number,
  subTotalVentas: number,
  descuExenta: number,
  descuGravada: number,
  porcentajeDescuento: number,
  totalDescu: number,
  ivaPerci1: number,
  ivaRete1: number,
  montoTotalOperacion: number,
  totalNoGravado: number,
  totalPagar: number,
  totalLetras: number,
  saldoFavor: number,
  dte?: DTE
}

export interface Emisor {
  Nit: string,
  Nombre: string,
  CodActividad: string,
  Departamento: string,
  Municipio: string,
  Complemento: string
  Correo: string,
  dte?:DTE[]

}
export interface EmisorR {
  nit: string,
  nombre: string,
  codActividad: string,
  departamento: string,
  municipio: string,
  complemento: string
  correo: string,
  dte?: DTER[]

}
export interface Receptor {
  Nit: string,
  Nombre: string,
  CodActividad: string,
  Departamento: string
  Municipio: string,
  Complemento: string
  Correo: string,
  TipoDocumento: string,
  dte?: DTE[]

}

export interface Receptor {
  Nit: string,
  Nombre: string,
  CodActividad: string,
  Departamento: string
  Municipio: string,
  Complemento: string
  Correo: string,
  TipoDocumento: string,
  dte?: DTE[]

}

export interface ReceptorR {
  nit: string,
  nombre: string,
  codActividad: string,
  departamento: string
  municipio: string,
  complemento: string
  correo: string,
  tipoDocumento: string,
  dte?: DTE[]

}
