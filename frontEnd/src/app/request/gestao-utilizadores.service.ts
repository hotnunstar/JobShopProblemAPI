import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { utilizadores } from '../gestao-utilizadores/gestaoUtilizadoresModel';
import { resposta } from '../gestao-utilizadores/respostaModel';
import { AutorServiceService } from './autor-service.service';

@Injectable({
  providedIn: 'root'
})
export class GestaoUtilizadoresService {

  baseUrl = 'https://localhost:7158/api/Utilizador';
  token = new HttpHeaders().append("Authorization", `Bearer ${this.tk.getToken()}`);
  
  
  constructor(private http:HttpClient,
              private tk: AutorServiceService) { }

  inserir(utilizador:any) : Observable<resposta>{ 
    return this.http.post<resposta>(`${this.baseUrl}`, utilizador);
  }
  
retornaUtilizadores(): Observable<utilizadores[]|resposta[]>
{
  return this.http.get<utilizadores[]|resposta[]>(this.baseUrl);
}

retornaUtilizadoresAdmin(): Observable<utilizadores[]>
{
  return this.http.get<utilizadores[]>(this.baseUrl);
}

  insereUtilizador(data:any,data1:any):Observable<utilizadores|resposta>
{
  
  let baseUrl=`https://localhost:7158/api/Utilizador?OldPassword=${data}&NewPassword=${data1}`;
  return this.http.put<utilizadores|resposta>(baseUrl,undefined);
}

DeleteUtilizador(utilizador:any,estado:boolean): Observable<resposta>
{
  let baseUrl = `https://localhost:7158/api/Utilizador/${estado}`;
  return this.http.put<resposta>(baseUrl, utilizador);
}
}
