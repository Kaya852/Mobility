# -*- coding: utf-8 -*-
from flask import Flask, request, jsonify
import pyodbc

app = Flask(__name__)

# Conenction information
server = '(localdb)\\MSSQLLocalDB'
database = 'MvcCrudContext-17d1b3da-70ac-4911-a47a-be8009ec39bf'
username = ''  # varsayýlan olarak boþ
password = ''  # varsayýlan olarak boþ

# Connect to SQL Server
conn = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER='+server+';DATABASE='+database+';UID='+username+';PWD='+ password)

@app.route('/api/Records/add', methods=['POST'])
def add_record():
    data = request.json
    cursor = conn.cursor()
    try:
        cursor.execute("INSERT INTO Record (name, surname, age) VALUES (?, ?, ?)", data['name'], data['surname'], data['age'])
        conn.commit()
        cursor.close()
        return jsonify({'message': 'Record added successfully'}), 201
    except Exception as e:
        return jsonify({'error': str(e)}), 500

@app.route('/api/Records/delete/<int:id>', methods=['DELETE'])
def delete_record(id):
    cursor = conn.cursor()
    try:
        cursor.execute("DELETE FROM Record WHERE id = ?", id)
        conn.commit()
        cursor.close()
        return jsonify({'message': 'Record deleted successfully'}), 200
    except Exception as e:
        return jsonify({'error': str(e)}), 500


@app.route('/api/Records/list', methods=['GET'])
def list_records():
    cursor = conn.cursor()
    try:
        cursor.execute("SELECT id, name, surname, age FROM Record")
        records = cursor.fetchall()
        cursor.close()
        return jsonify([{'id': record.id, 'name': record.name, 'surname': record.surname, 'age': record.age} for record in records]), 200
    except Exception as e:
        return jsonify({'error': str(e)}), 500

@app.route('/api/Records/search', methods=['GET'])
def search_records():
    name = request.args.get('name')
    cursor = conn.cursor()
    try:
        cursor.execute("SELECT id, name, surname, age FROM Record WHERE name = ?", name)
        records = cursor.fetchall()
        cursor.close()
        return jsonify([{'id': record.id, 'name': record.name, 'surname': record.surname, 'age': record.age} for record in records]), 200
    except Exception as e:
        return jsonify({'error': str(e)}), 500

if __name__ == '__main__':
    app.run(debug=True,host='localhost',port=5000)
