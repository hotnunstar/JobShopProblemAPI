import { Component, OnInit, ɵɵqueryRefresh } from '@angular/core';
import { Observable } from 'rxjs';
import { AtribuirTemposService } from '../request/atribuir-tempos.service';
import { TabelaProducaoService } from '../request/tabela-producao.service';
import { operacao } from '../tabela-producao/operacaoModel';
import { plano } from '../tabela-producao/planoModel';
import { simulacao } from '../tabela-producao/simulacaoModel';
import { TabelaProducaoComponent } from '../tabela-producao/tabela-producao.component';


@Component({
  selector: 'app-atribuir-tempos',
  templateUrl: './atribuir-tempos.component.html',
  styleUrls: ['./atribuir-tempos.component.css']
})
export class AtribuirTemposComponent implements OnInit {
planos:plano[]=[];
simulacoes:simulacao[]=[];
simulacao!: string;
fg:boolean = false;
fv:boolean = false;
tempoI!:number;
plano!:plano
index!:string;
  constructor(private service: AtribuirTemposService, private simul:TabelaProducaoService ) { }

  ngOnInit(): void {
    this.retorna();
  
  }
  validaSimulacao(id:any){
    this.simulacao = id;
  this.retornaPlanos();
      this.fg=true;

  }
  retornaPlanos(){
    this.service.retornaPlanos(this.simulacao).subscribe((result)=>{
      if("_response" in result)
      {
        alert(result._response + "\n" +"status code: " + result._statusCode);
      }else
        this.planos=result;
    
    });
  
  }
  
  retorna(){
     this.simul.retornaListaSimulacoes().subscribe((result)=>{
       
      if("_response" in result)
      {
        
        alert(result._response + "\n" +"status code: " + result._statusCode);
      }else
        this.simulacoes=result;
        
    });
  }
 modificaTempo(){
  this.service.inserePlanos(this.planos,this.planos[0].IdSimulacao).subscribe((result)=>{
      alert(result._response + " " + result._statusCode);  
  });
 }
 modifica(data:plano){
  this.fv =true;
  this.plano = data;
  console.log("soueu",this.plano);
 }
 modificaTemp()
 {
  for (var data in this.planos)
  {
    if(this.planos[data].IdJob === this.plano.IdJob && this.planos[data].IdSimulacao === this.plano.IdSimulacao && this.planos[data].IdOperacao === this.plano.IdOperacao)
    {
      this.planos[data].TempoInicial = this.tempoI;
      this.tempoI=0;
      this.index = data;
     
      
    }
  }
  this.fv=false;
   
 }
 refresh(): void {
  window.location.reload();
}

}
  

