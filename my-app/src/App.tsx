import React, { useEffect, useState } from 'react';
import { User } from './interfaces/User';

// Componente para listar usuários
const ListUsers: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    fetch('http://localhost:5102/get_users') // URL correta sem <>
      .then(response => {
        if (!response.ok) {
          throw new Error('Erro na requisição: ' + response.statusText);
        }
        return response.json();
      })
      .then(data => {
        setUsers(data);
      })
      .catch(error => {
        console.error('Erro:', error);
      });
  }, []);

  return (
    <div>
      <h1>Lista de Usuários</h1>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Idade</th>
          </tr>
        </thead>
        <tbody>
          {users.map(user => (
            <tr key={user.id}>
              <td>{user.id}</td>
              <td>{user.name}</td>
              <td>{user.age}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

// Componente principal
function App() {
  return (
    <div>
      <ListUsers />
    </div>
  );
}

export default App;
