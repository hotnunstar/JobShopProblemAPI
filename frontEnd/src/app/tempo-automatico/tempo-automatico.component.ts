import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Data } from 'popper.js';
import { AtribuirTemposService } from '../request/atribuir-tempos.service';
import { TabelaProducaoService } from '../request/tabela-producao.service';
import { plano } from '../tabela-producao/planoModel';
import { simulacao } from '../tabela-producao/simulacaoModel';

@Component({
  selector: 'app-tempo-automatico',
  templateUrl: './tempo-automatico.component.html',
  styleUrls: ['./tempo-automatico.component.css']
})
export class TempoAutomaticoComponent implements OnInit {
  simulacoes:simulacao[]=[];
  planos:plano[]=[];
  idSimul!:number;
  controlador:boolean=false;
  constructor(private service:AtribuirTemposService, private serviceTabelaProd:TabelaProducaoService,private route:Router) { }

  ngOnInit(): void {
    this.serviceTabelaProd.retornaListaSimulacoes().subscribe((result)=>{
      if("_response" in result)
      {
        
        alert(result._response + "\n" +"status code: " + result._statusCode);
        this.refresh();
      
      }else
        this.simulacoes=result;
   });
  }
  validaSimulacao(id:any)
  {
    this.idSimul=id
    
    this.service.insereTempoAutomatico(this.idSimul).subscribe((result=>{
      alert(result._response + "\n" + "status Code: " + result._statusCode);
    }))
  }
  refresh(): void {
    window.location.reload();
  }
  printarPlano()
  {
    this.service.retornaPlanos(this.idSimul).subscribe((result)=>{
      if("_response" in result)
      {
        this.route.navigate(['/home'])
        alert(result._response + "\n" +"status code: " + result._statusCode);
      }else
        this.planos=result;
        this.controlador=true;
      
   });

  }
  
}
