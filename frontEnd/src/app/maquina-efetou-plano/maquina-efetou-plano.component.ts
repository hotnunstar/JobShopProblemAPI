import { Component, OnInit } from '@angular/core';
import { resposta } from '../gestao-utilizadores/respostaModel';
import { MaquinaEfetuaPlanoService } from '../request/maquina-efetua-plano.service';
import { TabelaProducaoService } from '../request/tabela-producao.service';
import { job } from '../tabela-producao/jobModel';
import { maquina } from '../tabela-producao/maquinaModel';
import { operacao } from '../tabela-producao/operacaoModel';
import { plano } from '../tabela-producao/planoModel';
import { simulacao } from '../tabela-producao/simulacaoModel';

@Component({
  selector: 'app-maquina-efetou-plano',
  templateUrl: './maquina-efetou-plano.component.html',
  styleUrls: ['./maquina-efetou-plano.component.css']
})
export class MaquinaEfetouPlanoComponent implements OnInit {

  simulacoes:simulacao[]=[];
  maquinas:maquina[]=[];
  operacoes:operacao[]=[];
  plano!:plano;
  resposta!:resposta;
  jobs:job[]=[];
  IdJob:number=0;
  IdSimul:number=0;
  IdOperacao:number =0;
  IdMaquina:number =0;
  nomeJob!:string;
  nomeMaquina!:string;
  tempoMaquina!:number;
  controlador:boolean=false;
  constructor(private service: TabelaProducaoService, private ser: MaquinaEfetuaPlanoService) { }

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

  validaJob(id:any){
    this.IdJob = id;
    if(this.IdJob ===0)
    {
      alert("por favor preenche o job")
      this.refresh();
    }

  }
  validaOperacao(id:any){
    this.IdOperacao= id;
    if(this.IdOperacao ===0)
    {
      alert("por favor preenche a operação")
      this.refresh();
    }

  }
  validaSimulacao(id:any){
    this.IdSimul = id;
    if(this.IdSimul ===0)
    {
      alert("por favor preenche a simulação")
      this.refresh();
    }

  }
  RetornaPlano()
  {
    this.validaJob(this.IdJob);
    this.validaOperacao( this.IdOperacao);
    this.validaSimulacao(this.IdSimul);
    this.ser.RetornaPlano(this.IdSimul,this.IdOperacao,this.IdJob,).subscribe((result=>{
      if("_response" in result)
    {
     
      alert(result._response);
      alert(result._response + "\n" + "status Code: " + result._statusCode)
    }
    
    if("IdJob" in result)
    {
      console.log(this.retornaNomeMaquina(result.IdMaquina));

        this.nomeMaquina=this.retornaNomeMaquina(result.IdMaquina) || "";
        this.tempoMaquina=result.UnidadeTempo || 0;
        if( this.nomeMaquina!=="" && this.tempoMaquina!==0)
        {
          this.controlador=true;
        }
    }
    }));
  }
  retornaNomeMaquina(value:any)
  {
    for(var data in this.maquinas)
    {
      if(this.maquinas[data].Id === value)
      {
        return this.maquinas[data].Nome;
      }
    }
    return "";
  }
  refresh(): void {
    window.location.reload();
}
}
