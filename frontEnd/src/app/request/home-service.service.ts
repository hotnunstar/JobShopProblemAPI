import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HomeServiceService {

  constructor(private http:HttpClient) { }

  importarFicheiroPlano(id:any) {
    const url_endpoint = `https://localhost:7158/api/Plano/files/plano/${id}`;
    return this.http.get(url_endpoint, 
        {responseType: 'blob'});
}
importarFicheiroTabela(id:any) {
  const url_endpoint = `https://localhost:7158/api/Plano/files/tabela/${id}`;
  return this.http.get(url_endpoint, 
      {responseType: 'blob'});
}
}
