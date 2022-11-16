import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AtribuirTemposService } from '../request/atribuir-tempos.service';
import { plano } from '../tabela-producao/planoModel';
import { simulacao } from '../tabela-producao/simulacaoModel';

@Component({
  selector: 'app-lista-simulacoes',
  templateUrl: './lista-simulacoes.component.html',
  styleUrls: ['./lista-simulacoes.component.css']
})
export class ListaSimulacoesComponent implements OnInit {
  simulacoes:plano[]=[];
  controlador:boolean=false;
  IdSimulacao!:number;
  constructor(private service:AtribuirTemposService,private route:Router) { }

  ngOnInit(): void {
    this.retorna();
  }
  
  retorna(){
    this.service.retornaTodosPlanos().subscribe((result)=>{
      if("_response" in result)
      {
        this.route.navigate(['/home'])
        alert(result._response + "\n" +"status code: " + result._statusCode);
      }else
        this.simulacoes=result;
        this.controlador=true;
      
   });
 }
 refresh(): void {
  window.location.reload();
}
}
