import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AutorServiceService } from './autor-service.service';
import { job } from '../tabela-producao/jobModel';
import { Observable } from 'rxjs';
import { simulacao } from '../tabela-producao/simulacaoModel';
import { operacao } from '../tabela-producao/operacaoModel';
import { maquina } from '../tabela-producao/maquinaModel';
import { plano } from '../tabela-producao/planoModel';
import { resposta } from '../gestao-utilizadores/respostaModel';

@Injectable({
  providedIn: 'root'
})
export class TabelaProducaoService {
  token = new HttpHeaders().append("Authorization", `Bearer ${this.tk.getToken()}`);
  baseUrl1 = 'https://localhost:7158/api/Job';
  baseUrl2 = 'https://localhost:7158/api/Simulacao';
  baseUrl3 = 'https://localhost:7158/api/Operacao';
  baseUrl4 = 'https://localhost:7158/api/Maquina';
  baseUrl5 = 'https://localhost:7158/api/Plano';
  
  plano:any;
  constructor(private http:HttpClient ,private tk: AutorServiceService) { }

  retornaJobs(): Observable<job[]>
{
  return this.http.get<job[]>(this.baseUrl1, {headers: this.token});
}

insereJob(data:any): Observable<resposta>
{
  return this.http.post<resposta>(this.baseUrl1,data);
}


retornaListaSimulacoes(): Observable<simulacao[]|resposta>
{
  return this.http.get<simulacao[]|resposta>(this.baseUrl2, {headers: this.token});
}


insereSimulacao(data:any): Observable<resposta>
{
  return this.http.post<resposta>(this.baseUrl2,data);
}

retornaOperacoes(): Observable<operacao[]>
{
  return this.http.get<operacao[]>(this.baseUrl3, {headers: this.token});
}

insereOperacao(data:any): Observable<resposta>
{
  return this.http.post<resposta>(this.baseUrl3,data);
}

retornaMaquinas(): Observable<maquina[]>
{
  return this.http.get<maquina[]>(this.baseUrl4, {headers: this.token});
}

insereMaquina(data:any): Observable<resposta>
{
  return this.http.post<resposta>(this.baseUrl4,data);
}

inserirPlano(data:any) : Observable<resposta>{

  
  return this.http.post<resposta>(this.baseUrl5,data,{headers: this.token});
}
eliminarSimulacao(data:any):Observable<resposta>{
  let baseUrl2 = `https://localhost:7158/${data}`
  return this.http.delete<resposta>(baseUrl2);
}

alterarValorOperaçao(planos:any):Observable<resposta>
{
  let url = 'https://localhost:7158/AlterarValor'
  return this.http.put<resposta>(url,planos)
}


}
