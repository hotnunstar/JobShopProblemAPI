import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GestaoUtilizadoresComponent } from './gestao-utilizadores/gestao-utilizadores.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AtribuirTemposComponent } from './atribuir-tempos/atribuir-tempos.component';
import { TabelaProducaoComponent } from './tabela-producao/tabela-producao.component';
import { InserirDadosComponent } from './inserir-dados/inserir-dados.component';
import { MaquinaEfetouPlanoComponent } from './maquina-efetou-plano/maquina-efetou-plano.component';
import { ListaSimulacoesComponent } from './lista-simulacoes/lista-simulacoes.component';
import { ApagarSimulacoesComponent } from './apagar-simulacoes/apagar-simulacoes.component';
import { TempoAutomaticoComponent } from './tempo-automatico/tempo-automatico.component';
import { AlterarTabelaComponent } from './alterar-tabela/alterar-tabela.component';

const routes: Routes = [
  
  {path:'', component: LoginComponent},
  {path:'home', component: HomeComponent},
  {path:'tabelaProduçao', component: TabelaProducaoComponent},
  {path:'gestaoUtilizadores', component: GestaoUtilizadoresComponent},
  {path:'atribuirTempos', component: AtribuirTemposComponent},
  {path:'inserirDados', component: InserirDadosComponent},
  {path:'maquinaEfetouPlano', component: MaquinaEfetouPlanoComponent},
  {path:'listaSimulacoes', component: ListaSimulacoesComponent},
  {path:'apagarSimulacao', component: ApagarSimulacoesComponent},
  {path:'tempoAutomatico', component: TempoAutomaticoComponent},
  {path:'alterarTabela', component:  AlterarTabelaComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
