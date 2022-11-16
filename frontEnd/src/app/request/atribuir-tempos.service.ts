import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AtribuirTemposComponent } from '../atribuir-tempos/atribuir-tempos.component';
import { GestaoUtilizadoresComponent } from '../gestao-utilizadores/gestao-utilizadores.component';
import { resposta } from '../gestao-utilizadores/respostaModel';
import { LoginComponent } from '../login/login.component';
import { plano } from '../tabela-producao/planoModel';
import { AutorServiceService } from './autor-service.service';

@Injectable({
  providedIn: 'root'
})
export class AtribuirTemposService {
  token = new HttpHeaders().append("Authorization", `Bearer ${this.tk.getToken()}`);
 
  constructor(private http:HttpClient ,private tk: AutorServiceService) { }
 
  
  retornaPlanos(id:any):Observable<plano[]|resposta>
{
  
  let baseUrl6=`https://localhost:7158/api/Plano/${id}`;
  return this.http.get<plano[]|resposta>(baseUrl6, {headers: this.token})
}

retornaTodosPlanos():Observable<plano[]|resposta>
{
  
  let baseUrl6=`https://localhost:7158/simulacoes`;
  return this.http.get<plano[]|resposta>(baseUrl6, {headers: this.token})
}
inserePlanos(data:any,id:any):Observable<resposta>
{
  
  let baseUrl6=`https://localhost:7158/api/Plano/${id}`;
  return this.http.put<resposta>(baseUrl6,data)
}
insereTempoAutomatico(id:any):Observable<resposta>
{
  let baseUrl6=`https://localhost:7158/automatico/${id}`;
  return this.http.put<resposta>(baseUrl6,id)
}
}
