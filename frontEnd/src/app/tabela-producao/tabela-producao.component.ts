import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { LoginComponent } from '../login/login.component';
import { TabelaProducaoService } from '../request/tabela-producao.service';
import { job } from './jobModel';
import { maquina } from './maquinaModel';
import { operacao } from './operacaoModel';
import { plano } from './planoModel';
import { simulacao } from './simulacaoModel';

@Component({
  selector: 'app-tabela-producao',
  templateUrl: './tabela-producao.component.html',
  styleUrls: ['./tabela-producao.component.css']
})
export class TabelaProducaoComponent implements OnInit {
  jobs:job[]=[];
  planos:plano[]=[] 
 simulacoes:simulacao[]=[];
  maquinas:maquina[]=[];
  operacoes:operacao[]=[];
  plano:plano ={
    IdJob :0, //
    IdOperacao :0, //
    IdSimulacao:0, //
    IdMaquina:0, //
    IdUtilizador:0, //
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
  
 
  validaMaquina(id:any){
    this.plano.IdMaquina = id;
    console.log("--->", this.plano.IdMaquina)

  }
  validaJob(id:any){
    this.plano.IdJob = id;
    console.log("--->", this.plano.IdJob)

  }
  validaOperacao(id:any){
    this.plano.IdOperacao = id;
    console.log("--->", this.plano.IdJob)

  }
  validaSimulacao(id:any){
    this.plano.IdSimulacao = id;
    console.log("--->", this.plano.IdJob)

  }
  
 
  onSubmit(){
    console.log("--->", this.plano.UnidadeTempo);
     
  }
  InserePlano(){
    this.service.inserirPlano(this.plano).subscribe((result) =>{
      alert(result._response +" "+ "satus code: "+result._statusCode);
      this.refresh();
    });
  }
  getSimulacoes(){
    return this.simulacoes;

  }
  refresh(): void {
    window.location.reload();
  }
}

