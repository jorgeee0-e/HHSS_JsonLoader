import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Constant } from '../Constante/constantes';

@Injectable({
  providedIn: 'root'
})
export class DTESaveService {

  constructor(private http: HttpClient) { }

  getAllDTEs() {
    return this.http.get<{mensaje: string, response: any[]}>(Constant.API_END_POINT + Constant.METHODS.GET_ALL_DTES);
  }

  getEmisores() {
    return this.http.get<{mensaje: string, response: any[]}>(Constant.API_END_POINT + Constant.METHODS.GET_ALL_EMISORES);
  }

  getReceptores() {
    return this.http.get<{mensaje: string, response: any[]}>(Constant.API_END_POINT+Constant.METHODS.GET_ALL_RECEPTOR)
  }

  getResumen() {
    return this.http.get<{ mensaje: string, response: any[] }>(Constant.API_END_POINT+Constant.METHODS.GET_RESUMEN)
  }

  saveDTE(obj: any) {
    return this.http.post<any>(Constant.API_END_POINT+Constant.METHODS.CREATE_DTE,obj)
  }

  saveEmisor(obj: any) {
    return this.http.post<any>(Constant.API_END_POINT + Constant.METHODS.CREATE_EMISOR, obj)
  }

  saveReceptor(obj: any) {
    return this.http.post<any>(Constant.API_END_POINT + Constant.METHODS.CREATE_RECEPTOR, obj)
  }

  saveResumen(obj: any) {
    return this.http.post<any>(Constant.API_END_POINT + Constant.METHODS.CREATE_RESUMEN, obj)
  }
}
