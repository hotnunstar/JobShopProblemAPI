import { Component, OnInit } from '@angular/core';
import { TabelaProducaoService } from '../request/tabela-producao.service';
import { simulacao } from '../tabela-producao/simulacaoModel';

@Component({
  selector: 'app-apagar-simulacoes',
  templateUrl: './apagar-simulacoes.component.html',
  styleUrls: ['./apagar-simulacoes.component.css']
})
export class ApagarSimulacoesComponent implements OnInit {
  simulacoes:simulacao[]=[];
  idSimul!:number;
  constructor(private service: TabelaProducaoService) { }

  ngOnInit(): void {
    this.retornaSimulacoes()
  }

  retornaSimulacoes()
  {
    this.service.retornaListaSimulacoes().subscribe((result)=>{
      if("_response" in result)
      {
        
        alert(result._response + "\n" +"status code: " + result._statusCode);
        this.refresh();
      
      }else
        this.simulacoes=result;
   });

  }
  validaSimulacao(id:any){
    this.idSimul = id;
    

  }
  apagarSimulacao()
  {
    this.service.eliminarSimulacao(this.idSimul).subscribe((result=>{
      alert(result._response + "\n" +"status code: " + result._statusCode);
      this.refresh();
    }));
  }

  refresh(): void {
    window.location.reload();
  }
}
