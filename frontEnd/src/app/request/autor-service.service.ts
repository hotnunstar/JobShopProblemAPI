import { HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import jwt_decode from 'jwt-decode';
import { utilizadores } from '../gestao-utilizadores/gestaoUtilizadoresModel';



@Injectable({
  providedIn: 'root'
})
export class AutorServiceService {
  baseUrl = 'https://localhost:7158/api/';

  user: any;
  pass: any;
  data: any;
  
  constructor(private http:HttpClient) { }
  login(data:any) : Observable<any>{
    this.user = data.username;
    this.pass = data.password;
    
    return this.http.post<any>(`${this.baseUrl}JWTToken?Username=${data.username}&Password=${data.password}`, data);
}

// STORE the token in localstore:
setToken(token:string){
  localStorage.setItem('token', token);
  console.log("Armazenamento:", token);
}

// READ the token from localstorage and Deserialize
getToken(): string | null{
  let token = localStorage.getItem( 'token' );
  return token;
}

discardToken(): void{
  localStorage.removeItem( 'token' );
}

getUtilizadorObj(): Observable<any>
{
  return this.http.get<utilizadores>(`${this.baseUrl}Utilizador/${this.user}/${this.pass}`);
}

}