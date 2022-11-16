import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms'
import { Router } from '@angular/router';
import { toInteger } from '@ng-bootstrap/ng-bootstrap/util/util';
import { AutorServiceService } from '../request/autor-service.service';
import { GestaoUtilizadoresService } from '../request/gestao-utilizadores.service';
import{utilizadores} from './gestaoUtilizadoresModel'
import { resposta } from './respostaModel';



@Component({
  selector: 'app-gestao-utilizadores',
  templateUrl: './gestao-utilizadores.component.html',
  styleUrls: ['./gestao-utilizadores.component.css']
})
export class GestaoUtilizadoresComponent implements OnInit {
user: utilizadores = {
Username:"",
Password:"",
Admin:false,
Estado:true,
Id:0,

}
fv:boolean = false;
fg:boolean = false;
grupoTempo!: FormGroup
utili : utilizadores[] = []
utilizador! :utilizadores;
estado:boolean = false;
newPass!:string;
oldPass?:string;
response!:resposta;

  AutorServiceService: any;

  constructor( private service: GestaoUtilizadoresService, private router:Router,private login:AutorServiceService) { }
    
  ngOnInit(): void {
    this.mostras();
  }
  
  chek()
  {console.log(this.user)
    if(this.user)
    {
      this.service.inserir(this.user).subscribe((result) =>{ 
        alert(result._response + "\n" + "status Code: " + result._statusCode)
       }); 
  }
}

  mostras()
  {
    this.service.retornaUtilizadoresAdmin().subscribe((result) =>{
      this.utili=result
    });
  }

alteraEstado()
  {
      this.estado =true;
  }
  apagarUtilizador(uti:any)
  {
    
    this.utilizador=uti;
    console.log(this.utilizador);
    this.service.DeleteUtilizador(this.utilizador,this.utilizador.Estado).subscribe((result)=>{
        alert(result._response + "\n" + "status Code: " + result._statusCode);
        this.refresh();
    });
     }
     refresh(): void {
      window.location.reload();
  }
   }
   
  
  


