import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private apiUrl = 'http://localhost:5098/api';

  constructor(private http: HttpClient) {}

  // Pessoas
  getPessoas(): Observable<any> {
    return this.http.get(`${this.apiUrl}/Pessoa`);
  }

  getPessoa(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/Pessoa/${id}`);
  }

  addPessoa(pessoa: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Pessoa`, pessoa);
  }

  updatePessoa(id: number, pessoa: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/Pessoa/${id}`, pessoa);
  }

  deletePessoa(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Pessoa/${id}`);
  }

  // Tarefas
  getTarefas(): Observable<any> {
    return this.http.get(`${this.apiUrl}/Tarefa`);
  }

  getTarefa(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/Tarefa/${id}`);
  }

  addTarefa(tarefa: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Tarefa`, tarefa);
  }

  updateTarefa(id: number, tarefa: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/Tarefa/${id}`, tarefa);
  }

  deleteTarefa(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Tarefa/${id}`);
  }

  updateTarefaStatus(id: number, status: string): Observable<any> {
    return this.http.patch(`${this.apiUrl}/Tarefa/${id}/Status`, { status });
  }
}
