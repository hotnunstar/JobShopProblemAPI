import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { resposta } from '../gestao-utilizadores/respostaModel';
import { plano } from '../tabela-producao/planoModel';

@Injectable({
  providedIn: 'root'
})
export class MaquinaEfetuaPlanoService {

  constructor(private http:HttpClient) { }

  RetornaPlano(idS:any,idO:any,idJ:any ): Observable<plano|resposta>
  {
    let baseUrl6=`https://localhost:7158/api/Plano?IdSimulacao=${idS}&IdJob=${idJ}&IdOperacao=${idO}`;
    return this.http.get<plano|resposta>(baseUrl6);
  }
}
