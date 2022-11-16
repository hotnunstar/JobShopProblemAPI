import { Component, OnInit } from '@angular/core';
import { TabelaProducaoService } from '../request/tabela-producao.service';
import { job } from '../tabela-producao/jobModel';
import { maquina } from '../tabela-producao/maquinaModel';
import { operacao } from '../tabela-producao/operacaoModel';
import { plano } from '../tabela-producao/planoModel';
import { simulacao } from '../tabela-producao/simulacaoModel';

@Component({
  selector: 'app-alterar-tabela',
  templateUrl: './alterar-tabela.component.html',
  styleUrls: ['./alterar-tabela.component.css']
})
export class AlterarTabelaComponent implements OnInit {
  jobs:job[]=[];
  idJob!:number;
  unidadeTempo!:number; 
  simulacoes:simulacao[]=[];
  idSimul!:number;
  maquinas:maquina[]=[];
  idMaquina!:number;
  operacoes:operacao[]=[];
  idOperacao!:number;
  plano:plano ={
    IdJob :0, 
    IdOperacao :0, 
    IdSimulacao:0, 
    IdMaquina:0, 
    IdUtilizador:0, 
    TempoInicial:0,
    UnidadeTempo:0,
    TempoFinal:0,
    PosOperacao:0,
    Estado:true,
  };
  constructor(private service: TabelaProducaoService) { }

  ngOnInit(): void {
    this.carregaMaterial();
  }
  carregaMaterial():void {
    this.service.retornaJobs().subscribe((result)=>{
      this.jobs = result;
    });
    this.service.retornaListaSimulacoes().subscribe((result)=>{
      if("_response" in result)
      {
        
        alert(result._response + "\n" +"status code: " + result._statusCode);
        this.refresh();
      
      }else
        this.simulacoes=result;
   });

        this.service.retornaMaquinas().subscribe((result)=>{
          this.maquinas = result;
        });
        this.service.retornaOperacoes().subscribe((result)=>{
          this.operacoes = result;
          
        });
        

  }
  refresh(): void {
    window.location.reload();
  }
  validaMaquina(id:any){
    this.plano.IdMaquina = id;
   

  }
  validaJob(id:any){
    this.plano.IdJob = id;
    

  }
  validaOperacao(id:any){
    this.plano.IdOperacao = id;
    

  }
  validaSimulacao(id:any){
    this.plano.IdSimulacao = id;

  }
  onSubmit(){
    console.log("--->", this. unidadeTempo);
     
  }
 guardar()
  {
    this.plano.UnidadeTempo=this.unidadeTempo;
    console.log(this.plano);
    this.service.alterarValorOperaçao(this.plano).subscribe((result=>{
      alert(result._response +" "+ "satus code: "+result._statusCode);
      this.refresh();
    }));
  }
}
