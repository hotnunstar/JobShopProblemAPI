import { Component, OnInit } from '@angular/core';
import { TabelaProducaoService } from '../request/tabela-producao.service';
import { job } from '../tabela-producao/jobModel';
import { maquina } from '../tabela-producao/maquinaModel';
import { operacao } from '../tabela-producao/operacaoModel';
import { simulacao } from '../tabela-producao/simulacaoModel';

@Component({
  selector: 'app-inserir-dados',
  templateUrl: './inserir-dados.component.html',
  styleUrls: ['./inserir-dados.component.css']
})
export class InserirDadosComponent implements OnInit {
job:job={
  Id:0,
  Nome:""
};
maquina:maquina={
  Id:0,
  Nome:""
};
operacao:operacao={
  Id:0,
  Nome:""
};
simulacao:simulacao={
  Id:0,
  Nome:""
};
  constructor(private service:TabelaProducaoService) { }

  ngOnInit(): void {
  
  }

  guardaJob()
  {
      this.service.insereJob(this.job).subscribe((result=>{
        alert(result._response + "\n" + "Status Code: " + result._statusCode);
        this.refresh();
      }));
  }
  guardaMaquina()
  {
    
      this.service.insereMaquina(this.maquina).subscribe((result=>{
        alert(result._response + "\n" + "Status Code: " + result._statusCode);
        this.refresh();
      }));
  }
  guardaSimulacao()
  {
    
      this.service.insereSimulacao(this.simulacao).subscribe((result=>{
        alert(result._response + "\n" + "Status Code: " + result._statusCode);
        this.refresh();
      }));
  }
  guardaOperacao()
  {
  
      this.service.insereOperacao(this.operacao).subscribe((result=>{ 
        alert(result._response + "\n" + "Status Code: " + result._statusCode);
        this.refresh();
      }));
  }
  refresh(): void {
    window.location.reload();
}
}
